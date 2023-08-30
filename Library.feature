Feature: Library Manager API tests

@Passed
Scenario: 01 Library is empty
	Given URL address http://localhost:9000/api/books/
	When User sends GET request to http://localhost:9000/api/books/
	Then The response is 200 OK and empty body []
@Failed
Scenario:02 Add First book to empty Library
    Given URL address http://localhost:9000/api/books/
    When User sends POST request with JSON body:
	"""
  {"Id":1,"Title":"Lolita","Description":"1955","Author":"VladimirNabokov"}
    """
    Then The response is 200 OK and JSON body
    """
  {"Id":1,"Title":"Lolita","Description":"1955","Author":"VladimirNabokov"}
    """
@Failed
Scenario: 03 Add Second book to Library
    Given URL address http://localhost:9000/api/books/
    When User sends POST request with JSON body:
	"""
  {"Id":2,"Title":"TheNameoftheRose","Description":"1980","Author":"UmbertoEco"}
    """
    Then The response is 200 OK and JSON body
    """
  {"Id":2,"Title":"TheNameoftheRose","Description":"1980","Author":"UmbertoEco"}
    """
@Passed
Scenario: 04 Update a book from Library by Id
    Given URL address http://localhost:9000/api/books/1
    When User sends PUT request with JSON body:
	"""
  {"Id":1,"Title":"ForWhomtheBellTolls","Description":"1940","Author":"ErnestHemingway"}
    """
    Then The response is 200 OK and JSON body
    """
  {"Id":1,"Title":"ForWhomtheBellTolls","Description":"1940","Author":"ErnestHemingway"}
    """  
@Failed
Scenario: 05 Select a book from Library by Id
    Given URL address http://localhost:9000/api/books/1
    When User sends GET request to http://localhost:9000/api/books/1
    Then The response is 200 OK and JSON body
    """
  {"Id":1,"Title":"ForWhomtheBellTolls","Description":"1940","Author":"ErnestHemingway"}    
    """  
@Failed
Scenario: 06 Select a book from Library by Title
    Given URL address http://localhost:9000/api/books?Title=TheNameoftheRose
    When User sends GET request to http://localhost:9000/api/books?Title=TheNameoftheRose
    Then The response is 200 OK and JSON body
    """
[{"Id":2,"Title":"TheNameoftheRose","Description":"1980","Author":"UmbertoEco"}]
    """
@Passed
Scenario: 07 Delete First book from Library
    Given URL address http://localhost:9000/api/books/1
    When User sends DELETE request to http://localhost:9000/api/books/1
    Then The response is 204 No Content and empty body
@Passed
Scenario: 08 Delete Second book from Library
    Given URL address http://localhost:9000/api/books/2
    When User sends DELETE request to http://localhost:9000/api/books/2
    Then The response is 204 No Content and empty body
