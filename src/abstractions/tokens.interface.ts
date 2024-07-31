export interface ITokens {
    generateAccessToken<T>(payload: T): Promise<string>;

    generateRefreshToken<T>(payload: T): Promise<string>;

    extractFromToken<T>(token: T): Promise<number>;
};