export declare class FileService {
    upload(file: File, path: string, fileName: string, extensions?: string[]): Promise<string>;
    delele(): Promise<void>;
    getFile(): Promise<void>;
    checkFileExists(path: string): Promise<boolean>;
}
