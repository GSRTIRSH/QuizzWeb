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

export interface signinArgs {
    name: string
    password: string
}

export interface signupArgs {
    name: string
    email: string
    password: string
}

export interface getUserResponse {
    id: string
    userName: string
    normalizedUserName: string
    email: string
    normalizedEmail: string
    emailConfirmed: boolean
    passwordHash: string
    securityStamp: string
    concurrencyStamp: string
    phoneNumber: string
    phoneNumberConfirmed: boolean
    twoFactorEnabled: boolean
    lockoutEnd: string
    lockoutEnabled: boolean
    accessFailedCount: number
}
