FROM node:20-alpine

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

# Компиляция TypeScript → JS
RUN npx tsc -p ./tsconfig.json

EXPOSE 4000

CMD ["node", "dist/index.js"]
