export interface ICommentModel {
  id: string;
  commentText: string;
  rating: number;
  firstName: string;
  lastName: string;
  authorId: string;
}

export interface ICreateCommentModel {
  physicianId: string;
  commentText: string;
  rating: number;
}
