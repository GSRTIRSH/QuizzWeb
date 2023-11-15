export interface authResponse {
    result: boolean
}

export interface authResponseSuccess extends authResponse {
    token: string
    id: string
}

export interface authResponseError extends authResponse {
    errors: [string]
}

export interface loginArgs {
    name: string
    password: string
}

export interface signupArgs {
    name: string
    email: string
    password: string
}