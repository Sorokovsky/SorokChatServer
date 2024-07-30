import { Readable } from "node:stream";
import { rmdir, stat } from "fs/promises";
import * as nodePath from 'path';
import { FilesService } from "../files/files.service";

export const fakeFile: Express.Multer.File = {
    fieldname: "",
    originalname: "image.png",
    encoding: "",
    mimetype: "image/png",
    size: 0,
    stream: new Readable,
    destination: "",
    filename: "",
    path: "",
    buffer: Buffer.from("arrayBuffer")
};

export const tryRemoveFolder = async (path: string): Promise<void> => {
    try {
        const folderPath = nodePath.join(FilesService.STATIC_FOLDER, path);
        await stat(folderPath);
        await rmdir(folderPath);
    } catch (error) { };
};