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




build: ## Rebuilds all the containers
	docker build -t custom-base-image config/base
	docker-compose build

run: ## Start the containers
	docker network create app-network || true
	docker-compose up -d

stop: ## Stop the containers
	docker-compose stop

restart: ## Restart the containers
	$(MAKE) stop && $(MAKE) run	

run-test: ## Run test
	docker build -t custom-base-image config/base
	docker network create test-network || true
	docker-compose -f docker-compose.test.yml down --volumes --remove-orphans
	docker-compose -f docker-compose.test.yml build
	docker-compose -f docker-compose.test.yml run api-test
	dotnet build




logs: ## Show all logs
	docker-compose logs

api-bash: ## Entry api bash
	 docker-compose exec api bash

delete-containers: ## Remove all containers
	docker-compose down

delete-all-services: ## Remove all containers and volumes (***CAUTION***)
	docker-compose down -v
	docker system prune




SONAR_TOKEN=$(shell echo $$SONAR_TOKEN)
.PHONY: dn-ss

current_dir=$(shell pwd)

dn-ss: ## Run SonarCloud Scanner
	sed -i 's|/app/|$(current_dir)|g' coverage.xml
	dotnet sonarscanner begin /k:'wsmcbl_wsmcbl.back' /o:'wsmcblproyect2024' /d:sonar.token='$(SONAR_TOKEN)' /d:sonar.host.url='https://sonarcloud.io' /d:sonar.exclusions='**/*.sql, **/*Context.cs, **/Test*.cs' /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
	dotnet build --no-incremental
	dotnet sonarscanner end /d:sonar.token='$(SONAR_TOKEN)'
	rm -rf .sonarqube || true