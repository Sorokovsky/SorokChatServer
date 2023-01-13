import { Module } from '@nestjs/common';
import { MongooseModule } from '@nestjs/mongoose';
import { UsersModule } from './users/users.module';
import { config } from 'dotenv';
import { AuthorizationModule } from './authorization/authorization.module';
import { FileModule } from './file/file.module';
import { ServeStaticModule } from '@nestjs/serve-static';
import * as path from 'path';
config();
const DBURL:string = process.env.DBURL; 
@Module({
  imports: [ 
    MongooseModule.forRootAsync({
      useFactory: () => ({uri: DBURL})
    }),
    UsersModule, 
    AuthorizationModule,
    FileModule,
    ServeStaticModule.forRoot({rootPath: path.join(__dirname, 'static')})
  ]
})
export class AppModule {}; 
