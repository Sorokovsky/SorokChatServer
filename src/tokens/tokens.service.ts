import { Injectable } from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { ITokens } from 'src/abstractions/tokens.interface';

@Injectable()
export class TokensService implements ITokens{
    constructor(
        private readonly jwt: JwtService
    ) { };

    async generateAccessToken<T>(payload: T): Promise<string> {
        return await this.generateToken(payload, '15m');
    }

    async generateRefreshToken<T>(payload: T): Promise<string> {
        throw new Error('Method not implemented.');
    }

    async extractFromToken<T>(token: T): Promise<number> {
        throw new Error('Method not implemented.');
    }

    private async generateToken<T>(payload: T, expiryTime: number | string): Promise<string> {
        const token = await this.jwt.signAsync(JSON.stringify(payload));
        return token;
    }
};
