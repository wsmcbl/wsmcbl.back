#!/bin/bash

help: ## Show this help message
	@echo 'usage: make [target]'
	@echo
	@echo 'targets:'
	@egrep '^(.+)\:\ ##\ (.+)' ${MAKEFILE_LIST} | column -t -c 2 -s ':#'

b = unknown

mer-b2dev: ## Merge branch (b) into develop
	git checkout develop
	git merge --no-ff -m "Merge $(b) into develop" $(b)
	git checkout $(b)

mer-dev2b: ## Merge develop into branch (b)
	git checkout $(b)
	git merge --no-ff -m "Merge develop into $(b)" develop

mer-cur2dev: ## Merge current-branch into develop
	$(MAKE) mer-b2dev b=$(shell git rev-parse --abbrev-ref HEAD)

mer-cur2b: ## Merge current-branch into branch (b)
	$(MAKE) mer-cur2dev
	$(MAKE) mer-dev2b

git-create-master: ## Create master branch
	git branch -d master
	git branch master 

gpc: ## git push origin current brach
	git push origin $(shell git rev-parse --abbrev-ref HEAD)



build: ## Rebuilds all the containers
	docker build -t custom-base-image config/base
	docker-compose build

run: ## Start the containers
	docker-compose up -d

stop: ## Stop the containers
	docker-compose stop

restart: ## Restart the containers
	$(MAKE) stop && $(MAKE) run	

remake: ## Stop, build and run the containers
	$(MAKE) stop && $(MAKE) build && $(MAKE) run



logs: ## Show all logs
	docker-compose logs

api-bash: ## Entry api bash
	 docker-compose exec api bash

delete-containers: ## Remove all containers 
	docker-compose down





pg-restore:
	docker exec -i database_restore pg_restore -U $(DATABASE_USER) -d $(DATABASE_NAME) /backup.sql

restore.stop:
	docker-compose -f docker-compose.restore.yml stop

restore.build:
	docker build -t custom-base-image config/base
	docker-compose -f docker-compose.restore.yml build

restore.run:
	docker-compose -f docker-compose.restore.yml up -d	

remake-restore:
	$(MAKE) restore.stop && $(MAKE) restore.build && $(MAKE) restore.run

delete-all-services: ## Remove all containers and volumes (***CAUTION***)
	docker-compose -f docker-compose.yml -f docker-compose.restore.yml down --volumes --remove-orphans
	docker system prune
	docker network create app-network || true


SONAR_TOKEN=$(shell echo $$SONAR_TOKEN)
.PHONY: dn-ss

dn-ss: ## Run SonarCloud Scanner
	dotnet sonarscanner begin /k:'wsmcbl_wsmcbl.back' /o:'wsmcblproyect2024' /d:sonar.token='$(SONAR_TOKEN)' /d:sonar.host.url='https://sonarcloud.io' /d:sonar.exclusions='**/*.sql, **/*Context.cs' /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
	dotnet build --no-incremental
	dotnet sonarscanner end /d:sonar.token='$(SONAR_TOKEN)'

run-test: ## Run test
	dotnet-coverage collect 'dotnet test' -f xml -o 'coverage.xml'

sonar: ## Update sonar
	$(MAKE) run-test || $(MAKE) dn-ss
