import { Module } from '@nestjs/common';
import { ConfigModule, ConfigService } from '@nestjs/config';
import { TypeOrmModule } from '@nestjs/typeorm';
import { getTypeOrmConfiguration } from './config/typeorm.config';
import { UsersRepositoryModule } from './database/repositories/users-repository/users-repository.module';
import { FilesModule } from './files/files.module';
import { ChannelsRepositoryModule } from './database/repositories/channels-repository/channels-repository.module';

@Module({
  imports: [
    ConfigModule.forRoot(),
    TypeOrmModule.forRootAsync({
      imports: [ConfigModule],
      inject: [ConfigService],
      useFactory: getTypeOrmConfiguration
    }),
    UsersRepositoryModule,
    FilesModule,
    ChannelsRepositoryModule
  ]
})
export class AppModule {};
