import { Injectable } from '@nestjs/common';
import { JwtService, JwtSignOptions } from '@nestjs/jwt';
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
        return await this.generateToken(payload, '7d');
    }

    async extractFromToken<T extends object>(token: string): Promise<T> {
        return await this.jwt.verifyAsync<T>(token);
    }

    private async generateToken<T>(payload: T, expiryTime: number | string): Promise<string> {
        const token = await this.jwt.signAsync(JSON.stringify(payload), {expiresIn: expiryTime} as JwtSignOptions);
        return token;
    }
};
