export interface Student {
    id: number;
    name: string;
    score: number;
    grade: string;
}

export interface StudentCreate {
    name: string;
    score: number;
}

export interface User {
    username: string;
    token: string;
}
