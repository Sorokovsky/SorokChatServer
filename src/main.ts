import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { config } from 'dotenv';
if (config){
  config();
}
const PORT:number = Number(process.env.PORT) || 5001;
async function bootstrap() {
  const app = await NestFactory.create(AppModule);
  await app.listen(PORT, () => {console.log(`Server has been started on port: ${PORT}`);
  });
}
bootstrap();
