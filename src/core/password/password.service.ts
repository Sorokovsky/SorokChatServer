import { Injectable } from '@nestjs/common';
import { IPasswordService } from 'src/abstractions/password.interface';
import { hash, verify } from "argon2";
import { ConfigService } from '@nestjs/config';

@Injectable()
export class PasswordService implements IPasswordService {
    private readonly secret: string;
    constructor(
        config: ConfigService
    ) { 
        this.secret = config.get("HASH_SECRET_KEY")
    };

    async hash(rawPassword: string): Promise<string> {
        return await hash(rawPassword, {secret: this.secret});
    }

    async isEqual(rawPassword: string, hashedPassword: string): Promise<boolean> {
        return await verify(hashedPassword, rawPassword, {secret: this.secret});
    }
};
