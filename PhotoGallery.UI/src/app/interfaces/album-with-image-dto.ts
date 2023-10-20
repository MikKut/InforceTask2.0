import { AlbumDto } from "./album-dto.interface";
import { ImageDto } from "./image-dto";

export interface AlbumWithImageDto {
  albumDto: AlbumDto;
  image: ImageDto;
}
