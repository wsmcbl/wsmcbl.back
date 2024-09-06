#!/bin/bash

help: ## Show this help message
	@echo 'usage: make [target]'
	@echo
	@echo 'targets:'
	@egrep '^(.+)\:\ ##\ (.+)' ${MAKEFILE_LIST} | column -t -c 2 -s ':#'

b = unknown

git-mbd: ## Merge branch (b) into develop
	git checkout develop
	git merge --no-ff -m "Merge $(b) into develop" $(b)
	git checkout $(b)

git-mdb: ## Merge develop into branch (b)
	git checkout $(b)
	git merge --no-ff -m "Merge develop into $(b)" develop

git-mcd: ## Merge current-branch into develop
	$(MAKE) git-mbd b=$(shell git rev-parse --abbrev-ref HEAD)

git-mcb: ## Merge current-branch into branch (b)
	$(MAKE) git-mcd
	$(MAKE) git-mdb

git-master: ## make master branch
	git branch -d master
	git branch master 


run: ## Start the containers
	docker network create app-network || true
	docker-compose up -d

stop: ## Stop the containers
	docker-compose stop

restart: ## Restart the containers
	$(MAKE) stop && $(MAKE) run

build: ## Rebuilds all the containers
	docker build -t custom-base-image config/base
	docker-compose build    

logs: ## Show logs
	docker-compose logs
	
api-logs: ## show logs by container
	 docker-compose logs api
	 
api-bash: ## entry api bash
	 docker-compose exec api bash
 
dc-all: ## stop, build, and run container
	$(MAKE) stop && $(MAKE) build && $(MAKE) run
	
test: ## run test
	#docker build -t custom-base-image config/base
	docker network create test-network || true
	docker-compose -f docker-compose.test.yml build
	docker-compose -f docker-compose.test.yml run --rm api-test

delete-all: ## Remove all containers
	docker-compose down

delete-all-services: ## Remove all containers and volumes (***CAUTION***)
	docker-compose down -v
	docker system prune 
	

SONAR_TOKEN=$(shell echo $$SONAR_TOKEN)

.PHONY: dn-ss

dn-ss: ##Este es el nuevo
	dotnet sonarscanner begin /k:'wsmcbl_wsmcbl.back' /o:'wsmcblproyect2024' /d:sonar.token='$(SONAR_TOKEN)' /d:sonar.host.url='https://sonarcloud.io' /d:sonar.sources='src/' /d:sonar.inclusions='src/**/*.cs'  /d:sonar.exclusions='**/*.sql, **/*Context.cs, **/PublicProgram.cs' /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
	dotnet build --no-incremental
	dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
	dotnet sonarscanner end /d:sonar.login="$(SONAR_TOKEN)"
	rm -rf .sonarqube || true