import { Injectable } from '@nestjs/common';
import { getContext } from 'src/utils/context-storage';
import { ITokensStorage } from '../abstractions/tokens-storage.interface';

@Injectable()
export class BearerStorageService implements ITokensStorage {
    private readonly BEARER = "Bearer";
    private readonly AUTHORIZATION = "authorization";

    async setToken(token: string): Promise<void> {
        const { request, response } = await getContext();
        request.headers.authorization = `${this.BEARER} ${token}`;
        response.header(this.AUTHORIZATION, `${this.BEARER} ${token}`);
    }

    async tryGetToken(): Promise<string | null> {
        const { request } = await getContext();
        const authorization = request.headers.authorization;
        if (authorization === undefined || authorization === null) return null;
        const path: string[] = authorization.split(" ");
        if (path.length !== 2 || !path[0].startsWith(this.BEARER)) return null;
        return path[1];
    }

    async removeToken(): Promise<void> {
        const { request, response } = await getContext();
        request.headers.authorization = "";
        response.header(this.AUTHORIZATION, "");
    }
};
