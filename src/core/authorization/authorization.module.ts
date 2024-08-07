import { Module, Global } from '@nestjs/common';
import { UsersModule } from '../users/users.module';
import { AuthorizationService } from './authorization.service';
import { TokensModule } from '../../tokens/tokens.module';
import { CookieStorageModule } from '../../cookie-storage/cookie-storage.module';
import { BearerStorageModule } from '../../bearer-storage/bearer-storage.module';
import { PasswordModule } from '../password/password.module';

@Global()
@Module({
  imports: [UsersModule, TokensModule, CookieStorageModule, BearerStorageModule, PasswordModule],
  providers: [AuthorizationService],
  exports: [AuthorizationService]
})
export class AuthorizationModule { };
