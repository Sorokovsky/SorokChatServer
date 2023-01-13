import { Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { UsersModule } from './users/users.module';
import { config } from 'dotenv';
config();
const DBURL:string = process.env.DBURL; 
@Module({
  imports: [ 
    MongooseModule.forRootAsync({
      useFactory: () => ({uri: DBURL})
    }),
    UsersModule, 
  ]
})
export class AppModule {}; 
