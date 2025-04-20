'use client'
import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination"


type Props = {
  totalItems: number;
  itemsPerPage: number;
  currentPage: number;
  setCurrentPage: (page: number) => void;
}

export default function PaginationDemo({ 
  totalItems,
  itemsPerPage,
  currentPage,
  setCurrentPage}: Props) {

    let pages = [];
    for (let i=1; i<=Math.ceil(totalItems / itemsPerPage);i++){
      pages.push(i);
    }
    function handlePrevPage(): void {
      if (currentPage > 1) {
        setCurrentPage(currentPage - 1); // go back
      }
    }
    
    function handleNextPage(): void {
      if (currentPage < pages.length) {
        setCurrentPage(currentPage + 1); // go forward
      }
    }
    

  return (
    <Pagination>
      <PaginationContent>
        <PaginationItem>
          <PaginationPrevious onClick={()=> handlePrevPage()}/>
        </PaginationItem>
        {pages.map((page, idx)=>(
          <PaginationItem
          key={idx}
          className={currentPage === page ? "bg-neutral-100 rounded-md" : ""}>
            <PaginationLink onClick={() => setCurrentPage(page)}>
               {page}
            </PaginationLink>
          </PaginationItem>
        ))}
        <PaginationItem>
          <PaginationLink onClick={()=> handleNextPage()}/>
        </PaginationItem>
      </PaginationContent>
    </Pagination>
  )
}
