import "./searchPage.component.style.css";
import { Button, Input, TextField } from "@mui/material";

function SearchPage() {
  return (
    <div className="search-wrap">
      <TextField
        className="search-box"
        spellCheck={true}
        placeholder="шукай тут..."
        label="Пошук"
      ></TextField>
      <Button className="search-button">🔍</Button>
    </div>
  );
}

export default SearchPage;
