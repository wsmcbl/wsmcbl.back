#!/bin/bash

help: ## Show this help message
	@echo 'usage: make [target]'
	@echo
	@echo 'targets:'
	@egrep '^(.+)\:\ ##\ (.+)' ${MAKEFILE_LIST} | column -t -c 2 -s ':#'


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

logs: ## Show all logs
	docker-compose logs

api-bash: ## Entry api bash
	 docker-compose exec api bash

delete-containers: ## Remove all containers 
	docker-compose down

delete-all-services: ## Remove all containers and volumes (***CAUTION***)
	docker-compose down --volumes --remove-orphans
	docker system prune