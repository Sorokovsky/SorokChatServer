import { Injectable } from '@nestjs/common';
import { Request, Response } from 'express';
import { IncomingHttpHeaders } from 'http';
import { Context, getContext } from 'src/utils/context-storage';
import { ITokensStorage } from '../abstractions/tokens-storage.interface';

@Injectable()
export class BearerStorageService implements ITokensStorage {
    async setToken(token: string): Promise<void> {
        const { request, response } = await this.getContext();
        request.headers.authorization = `Bearer ${token}`;
        response.header("authorization", `Bearer ${token}`);
    }

    async tryGetToken(): Promise<string | null> {
        const { request } = await this.getContext();
        const authorization = request.headers.authorization;
        if (authorization === undefined || authorization === null) return null;
        const path: string[] = authorization.split(" ");
        if (path.length !== 2 || !path[0].startsWith("Bearer")) return null;
        return path[1];
    }

    async removeToken(): Promise<void> {
        const { request, response } = await this.getContext();
        request.headers.authorization = "";
        response.header("authorization", "");
    }
    
    private async getContext(): Promise<Context> {
        return await getContext();
    }
};
