import { Module } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { TokensService } from './tokens.service';

@Module({
  providers: [TokensService, JwtService]
})
export class TokensModule { };
