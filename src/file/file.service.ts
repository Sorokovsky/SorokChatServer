import { HttpException, HttpStatus, Injectable } from "@nestjs/common";
import * as fs from 'fs';
import * as mongoose from "mongoose";
import * as path from 'path';
@Injectable()
export class FileService {
    async uploadAvatar(avatar:Express.Multer.File, id: mongoose.Types.ObjectId):Promise<string>{
        try {
            const avatarPath:string = path.join(`${id}`, `avatar.${avatar.originalname.split('.').pop()}`);
            const avatarUrl:string = path.resolve(__dirname, '..', 'static', avatarPath);
            const avatarDirUrl:string = path.resolve(__dirname ,'..', 'static', `${id}`);
            if(!fs.existsSync(avatarDirUrl)) fs.mkdirSync(avatarDirUrl, {recursive:true});
            const prevAvatar = await this.checkAvatar(avatarDirUrl);
            if(prevAvatar) path.join(avatarDirUrl, prevAvatar)
            fs.writeFileSync(avatarUrl, avatar.buffer);
            return avatarPath;
        } catch (error) {
            throw new HttpException(error.message, HttpStatus.BAD_REQUEST);
        }
    }
    async checkAvatar(dir):Promise<string>{
        const items = fs.readdirSync(dir);
        for (let i = 0; i < items.length; i++) {
            const el = items[i];
            if (el.startsWith('avatar')) {
                return el;
            }
        }
        return null;
    }
}