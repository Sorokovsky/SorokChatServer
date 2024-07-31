import { Module, Global } from '@nestjs/common';
import { UsersModule } from '../users/users.module';
import { AuthorizationService } from './authorization.service';
import { TokensModule } from '../../tokens/tokens.module';

@Global()
@Module({
  imports: [UsersModule, TokensModule],
  providers: [AuthorizationService]
})
export class AuthorizationModule { };
