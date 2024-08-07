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

dn-ss: ## dotnet sonarscanner
	dotnet sonarscanner begin /k:'wsmcbl_wsmcbl.back' /o:'wsmcblproyect2024' /d:sonar.token='$(SONAR_TOKEN)' /d:sonar.host.url='https://sonarcloud.io' /d:sonar.exclusions='**/*.sql, **/*Context.cs, **/Test*.cs, **/PublicProgram.cs' /d:sonar.cs.vscoveragexml.reportsPaths=coverage.xml
	dotnet build --no-incremental
	dotnet-coverage collect "dotnet test" -f xml -o "coverage.xml"
	dotnet sonarscanner end /d:sonar.token='$(SONAR_TOKEN)'
	rm -rf .sonarqube || true

dn-mig: ## dotnet migrations
	cd src && rm -rf Migrations
	cd src && dotnet ef migrations add InitialCreate


run: ## Start the containers
	docker network create app-network || true
	docker-compose up -d

stop: ## Stop the containers
	docker-compose stop

restart: ## Restart the containers
	$(MAKE) stop && $(MAKE) run

build: ## Rebuilds all the containers
	docker-compose build    

logs: ## Show logs
	docker-compose logs
