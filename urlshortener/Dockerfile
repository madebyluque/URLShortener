FROM node:18-alpine AS build

WORKDIR /front
COPY ./urlshortener/package*.json ./
RUN npm install
COPY ./urlshortener ./
RUN npm run build

FROM nginx:alpine
COPY --from=build /front/dist /usr/share/nginx/html
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]
