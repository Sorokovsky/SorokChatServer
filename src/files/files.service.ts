import { Injectable } from '@nestjs/common';
import * as nodePath from 'path';
import { stat, mkdir, writeFile, rm, rmdir } from "fs/promises";
import { IFilesService } from 'src/abstractions/files.interface';

@Injectable()
export class FilesService implements IFilesService {
    public static readonly STATIC_FOLDER = nodePath.join(nodePath.resolve(), 'static');

    async upload(folder: string, file: Express.Multer.File): Promise<string> {
        const folderPath = nodePath.join(FilesService.STATIC_FOLDER, folder);
        const filePath = nodePath.join(folderPath, file.originalname);
        const isExists = await this.isExists(filePath);
        if (!isExists) await mkdir(folderPath, { recursive: true });
        await writeFile(filePath, file.buffer);
        return nodePath.join(folder, file.originalname);
    }

    async delete(deletePath: string): Promise<void> {
        const path: string = nodePath.join(FilesService.STATIC_FOLDER, deletePath);
        const isExists = await this.isExists(path);
        
        if (isExists) {
            const stats = await stat(path);
            if (stats.isDirectory()) await rmdir(path, { recursive: true });
            else if (stats.isFile()) await rm(path, { recursive: true });
        }
    }

    private async isExists(path: string): Promise<boolean> {
        try {
            await stat(path);
            return true;
        } catch (error) {
            return false;
        }
    }
};
