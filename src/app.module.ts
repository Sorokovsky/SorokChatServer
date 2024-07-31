import { Module } from '@nestjs/common';
import { ConfigModule, ConfigService } from '@nestjs/config';
import { TypeOrmModule } from '@nestjs/typeorm';
import { getTypeOrmConfiguration } from './config/typeorm.config';
import { UsersRepositoryModule } from './database/repositories/users-repository/users-repository.module';
import { FilesModule } from './files/files.module';
import { ChannelsRepositoryModule } from './database/repositories/channels-repository/channels-repository.module';
import { MessagesRepositoryModule } from './database/repositories/messages-repository/messages-repository.module';
import { UsersModule } from './core/users/users.module';
import { TokensModule } from './tokens/tokens.module';
import { JwtModule } from "@nestjs/jwt";
import { getJwtConfiguration } from './config/jwt.config';

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
    ChannelsRepositoryModule,
    MessagesRepositoryModule,
    UsersModule,
    TokensModule,
    JwtModule.registerAsync({
      imports: [ConfigModule],
      inject: [ConfigService],
      useFactory: getJwtConfiguration
    }),
  ]
})
export class AppModule {};
