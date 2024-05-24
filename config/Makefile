#!/bin/bash

DOCKER_BACK = wsmcbl-back
UID = $(shell id -u)

help: ## Show this help message
	@echo 'usage: make [target]'
	@echo
	@echo 'targets:'
	@egrep '^(.+)\:\ ##\ (.+)' ${MAKEFILE_LIST} | column -t -c 2 -s ':#'

start: ## Start the containers
	docker network create wsmcbl-back-network || true
	U_ID=${UID} docker-compose up -d

stop: ## Stop the containers
	U_ID=${UID} docker-compose stop

restart: ## Restart the containers
	$(MAKE) stop
	$(MAKE) start

build: ## Rebuilds all the containers
	U_ID=${UID} docker-compose build

ssh-be: ## ssh's into the be container
	U_ID=${UID} docker exec -it --user ${UID} ${DOCKER_BACK} bash


# Dotnet commands
dn-pub: ## Dotnet publish command
	U_ID=${UID} docker exec -it --user ${UID} ${DOCKER_BACK} rm -rf bin publish || true
	U_ID=${UID} docker exec -it --user ${UID} ${DOCKER_BACK} dotnet publish "wsmcbl.back.csproj" -c Release -o publish 


swagger: ## Make the Swagger documentation file
	cd .. && rm swagger.json || true && wget --no-check-certificate https://localhost:7211/swagger/v1/swagger.json || true