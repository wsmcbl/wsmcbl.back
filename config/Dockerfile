FROM mcr.microsoft.com/dotnet/sdk:8.0

ARG UID
RUN adduser -u ${UID} --disabled-password --gecos "" appuser
RUN mkdir /home/appuser/.ssh
RUN chown -R appuser:appuser /home/appuser/
RUN echo "StrictHostKeyChecking no" >> /home/appuser/.ssh/config
RUN echo "export COLUMNS=300" >> /home/appuser/.bashrc

RUN apt update
RUN apt install -y git acl openssl openssh-client wget zip vim libssh-dev

WORKDIR /www
COPY . .
RUN dotnet restore

ENTRYPOINT ["dotnet", "./publish/wsmcbl.back.dll"]