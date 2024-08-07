import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';
import { AsyncLocalStorage } from 'async_hooks';
import * as cookieParser from "cookie-parser";

const port = process.env.PORT || 5000;
export const storage = new AsyncLocalStorage();

async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  app.use(cookieParser());
  app.setGlobalPrefix("api");

  const config = new DocumentBuilder()
    .setTitle('SorokChat')
    .setDescription('Sorok Chat the best messanger ever!')
    .setVersion('1.0')
    .addBearerAuth()
    .build();
  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('swagger', app, document);

  await app.listen(port);
  
}
bootstrap();
