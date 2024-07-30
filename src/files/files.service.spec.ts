import { Test } from "@nestjs/testing";
import { FilesService } from './files.service';
import * as nodePath from "path";
import { readdir, writeFile } from "fs/promises";
import { fakeFile, tryRemoveFolder } from "../utils/fakes";
import { mkdir } from "fs/promises";

describe("Files service", () => {
    let filesService: FilesService;
    const folder: string = 'folder';
    
    beforeEach(async () => {
        const module = await Test.createTestingModule({
            providers: [FilesService],
        }).compile();

        filesService = module.get<FilesService>(FilesService);
        await tryRemoveFolder(folder);
    });

    afterAll(async () => {
        tryRemoveFolder(folder);
    });

    describe("createFile", () => {
        const path: string = nodePath.join(folder, fakeFile.originalname);
        it("should return path", async () => {
            const result = await filesService.upload(folder, fakeFile);
            expect(result).toEqual(path);
        });
    });

    describe("delete folder", () => {
        const path: string = nodePath.join(FilesService.STATIC_FOLDER, folder);
        it("should delete the folder", async () => {
            await mkdir(path, { recursive: true });
            await filesService.delete(folder);
            const items = await readdir(FilesService.STATIC_FOLDER);
            const hasFolder = items.includes(folder);            
            expect(hasFolder).toBeFalsy();      
        });
    });

    describe("delete file", () => {
        it("should delete the file", async () => {
            const path: string = nodePath.join(FilesService.STATIC_FOLDER, folder);
            const filePath: string = nodePath.join(path, fakeFile.originalname);
            await mkdir(path, { recursive: true });
            await writeFile(filePath, fakeFile.buffer);
            await filesService.delete(nodePath.join(folder, fakeFile.originalname));
            const items = await readdir(path);
            const hasFile = items.includes(fakeFile.originalname);            
            expect(hasFile).toBeFalsy();
        });
    });
});