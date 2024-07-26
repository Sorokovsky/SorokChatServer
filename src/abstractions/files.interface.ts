export interface IFilesService {
    upload(folder: string, file: Express.Multer.File): Promise<string>;

    delete(path: string): Promise<void>;
}