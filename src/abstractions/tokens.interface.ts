export interface ITokens {
    generateAccessToken<T>(payload: T): Promise<string>;

    generateRefreshToken<T>(payload: T): Promise<string>;

    extractFromToken<T extends object>(token: string): Promise<T>;
};