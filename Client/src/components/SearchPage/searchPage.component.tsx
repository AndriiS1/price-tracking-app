import { useState } from "react";
import "./searchPage.component.style.css";
import { Button, Input, TextField } from "@mui/material";
import productService from "../../Services/product.service";
import { SearchResponse } from "../../Services/Dtos/search.response";
import MediaCard from "./searchCard.component";

function SearchPage() {
  const [productName, setProductName] = useState<string>();
  const [response, setResponse] = useState<SearchResponse[]>();

  async function HandlerSearch() {
    if (productName) {
      let response = await productService.Search(productName);
      console.log(productName);
      console.log(response);
      setResponse(response);
    }
  }
  return (
    <div>
      <div className="search-wrap">
        <TextField
          onChange={(e) => setProductName(e.target.value)}
          className="search-box"
          spellCheck={true}
          placeholder="шукай тут..."
          label="Пошук"
        ></TextField>
        <Button onClick={HandlerSearch} className="search-button">
          🔍
        </Button>
      </div>{" "}
      <div className="response-wrap">
        {response &&
          response.map((searchResponse) => (
            <MediaCard response={searchResponse}></MediaCard>
          ))}
      </div>
    </div>
  );
}

export default SearchPage;
