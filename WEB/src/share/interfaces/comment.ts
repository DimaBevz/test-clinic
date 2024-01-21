export interface CommentModel {
	id: string
	commentText: string
	rating: number
	firstName: string
	lastName: string
	authorId: string
}

export interface CreateCommentModel {
	physicianId: string
	commentText: string
	rating: number
}
