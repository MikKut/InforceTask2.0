export interface ImageDto {
  extension: string;
  title: string;
  description: string;
  content: Uint8Array;
  likes: number;
  dislikes: number;
}
