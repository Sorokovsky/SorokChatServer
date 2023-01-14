/// <reference types="multer" />
import * as mongoose from "mongoose";
export declare class FileService {
    uploadAvatar(avatar: Express.Multer.File, id: mongoose.Types.ObjectId): Promise<string>;
    checkAvatar(dir: string): Promise<string>;
    deleteAvatar(id: mongoose.Types.ObjectId): Promise<void>;
}
