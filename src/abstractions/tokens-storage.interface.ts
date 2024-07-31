export interface ITokensStorage {
    setToken(token: string): Promise<void>;

    tryGetToken(): Promise<string | null>;

    removeToken(): Promise<void>;
};