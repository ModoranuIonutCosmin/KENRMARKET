export class PaginationHelpers {


  public static paginateArray<T>(items: Array<T>, itemsPerChunk: number) : Array<Array<T>> {

    var result = new Array<Array<T>>();

    for (let i = 0; i < items.length; i += itemsPerChunk) {
      const chunk = items.slice(i, i + itemsPerChunk);
      // do whatever

      result.push(chunk);
    }

    return result;
  }
}
