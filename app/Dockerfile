FROM node:20-slim AS deps

WORKDIR /app

COPY package.json ./
RUN npm install --package-lock-only

FROM node:20-slim

WORKDIR /app

COPY package.json ./
COPY --from=deps /app/package-lock.json ./
RUN npm install

COPY . .

EXPOSE 5173

CMD ["npm", "run", "dev", "--", "--host"]