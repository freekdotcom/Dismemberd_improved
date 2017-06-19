version: 0.6f
bgColor: #607080
alphabet:
name: "Specifications"
position: (-68,-142)

module:
name: "SpecifyDungeon"
alphabet: "Specifications"
position: (-120,26)
type: Recipe
match: None
grammar: true
recipe: true
showMembers: true

module:
name: "CreateStart"
alphabet: "Tiles"
position: (-104,142)
type: Recipe
match: None
recipe: true
showMembers: true

module:
name: "BuildDungeon"
alphabet: "Tiles"
position: (-2,94)
type: Recipe
match: UseAsRecipe
inputs: "CreateStart" "SpecifyDungeon"
grammar: true
showMembers: true

alphabet:
name: "Tiles"
position: (22,-138)

module:
name: "CreateDungeon"
alphabet: "Tiles"
position: (115,92)
type: Recipe
match: None
inputs: "BuildDungeon"
recipe: true
showMembers: true

option: Width 32
option: Height 32
option: Tile "undefined"
option: IncludeEdges true
