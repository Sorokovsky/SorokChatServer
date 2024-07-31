export interface IPasswordService {
    hash(rawPassword: string): Promise<string>;

    isEqual(rawPassword: string, hashedPassword: string): Promise<boolean>;
};