import { Injectable } from '@nestjs/common';
import { ITokensStorage } from 'src/abstractions/tokens-storage.interface';
import { getContext } from '../utils/context-storage';

@Injectable()
export class CookieStorageService implements ITokensStorage {
    private readonly REFRESH_TOKEN = "refresh_token";
    private readonly MAX_AGE = 1000 * 60 * 60 * 24 * 7;

    async setToken(token: string): Promise<void> {
        const { response } = await getContext();
        response.cookie(this.REFRESH_TOKEN, token, {
            httpOnly: true,
            maxAge: this.MAX_AGE
        });
    }

    async tryGetToken(): Promise<string | null> {
        const { request } = await getContext();
        const token: string | undefined | null = await request.cookies[this.REFRESH_TOKEN];
        if (token === undefined || token === null) return null;
        return token;
    }

    async removeToken(): Promise<void> {
        const { response } = await getContext();
        response.clearCookie(this.REFRESH_TOKEN);
    }
};
