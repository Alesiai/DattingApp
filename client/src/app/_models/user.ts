export interface User {
    username: string;
    token: string;
    photoUrl: string;
    knownAs: string;
    gender: string;
    isBlocked : boolean;
    roles: string[];
}