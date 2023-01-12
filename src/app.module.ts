import { Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { UsersModule } from './users/users.module';
const DBURL:string = process.env.DBURL; 
@Module({
  imports: [ 
    MongooseModule.forRootAsync({
      useFactory: () => ({uri: "mongodb://127.0.0.1:27017/sorokchat"})
    }),
    UsersModule, 
  ]
})
export class AppModule {}; 
