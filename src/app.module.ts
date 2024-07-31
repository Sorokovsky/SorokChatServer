import { MiddlewareConsumer, Module, NestModule } from '@nestjs/common';
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
import { PasswordModule } from './core/password/password.module';
import { AuthorizationModule } from './core/authorization/authorization.module';
import { BearerStorageModule } from './bearer-storage/bearer-storage.module';
import { ContextMiddleware } from './middlewares/context.middleware';
import { CookieStorageModule } from './cookie-storage/cookie-storage.module';

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
    PasswordModule,
    AuthorizationModule,
    BearerStorageModule,
    CookieStorageModule,
  ]
})
export class AppModule implements NestModule {
  configure(consumer: MiddlewareConsumer) {
    consumer.apply(new ContextMiddleware().use).forRoutes('*');
  }
};
