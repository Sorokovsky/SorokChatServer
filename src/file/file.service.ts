import { Injectable } from "@nestjs/common";
import * as fs from 'fs';
import * as pathNode from 'path';
@Injectable()
export class FileService {
    async upload(file:File, path:string, fileName:string, extensions?:string[]):Promise<string>{
        const filePath = pathNode.resolve(path, fileName);
        if(this.checkFileExists(filePath)) throw new Error('File setted');
        return filePath;
    }
    async delele(){

    }
    async getFile(){

    }
    async checkFileExists(path:string):Promise<boolean>{
        return new Promise((resolve, reject) => {
            fs.access(path, fs.constants.F_OK, err => {
                resolve(!err);
            })
        });
    }
}