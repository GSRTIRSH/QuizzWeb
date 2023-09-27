import { getListOfQuizzes } from "@/api/getListOfQuizzes"

export const useListOfQuizzes = async() => {
    try {
        const data = await getListOfQuizzes('BASH', 'easy');
        console.log(data)
    } catch (err) {
        alert(`Oops, ${err}`)
    }
}