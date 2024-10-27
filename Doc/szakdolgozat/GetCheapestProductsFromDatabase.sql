Select 
    CategoryName, 
    ComputerPartName, 
    Description, 
    ComputerPartPrice, 
    Currency, 
    WebShopURL, 
    ProductUrl 
From
    (
        Select
            s.Id, 
            cp.ComputerPartName, 
            c.CategoryName, 
            s.Description, 
            s.ComputerPartPrice, 
            s.Currency, 
            w.WebShopURL, 
            s.ProductUrl, 
            Row_Number() Over (Partition By cp.Id Order By s.ComputerPartPrice) AS RowNum 
        From 
            SearchResults s 
        Inner Join ComputerParts cp On s.ComputerPartId = cp.Id 
        Inner Join Categories c On cp.CategoryId = c.Id 
        Inner Join WebShops w On s.WebShopId = w.Id 
    ) AS ranked 
Where
    RowNum = 1;
