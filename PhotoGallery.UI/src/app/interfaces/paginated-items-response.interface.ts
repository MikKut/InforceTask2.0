export interface PaginatedItemsResponse<T> {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T[];
}
