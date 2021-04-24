FROM node:lts-alpine

WORKDIR /app

COPY package*.json ./

RUN npm install
RUN npm install -g @vue/cli

EXPOSE 2000

CMD ["npm", "run", "serve"]