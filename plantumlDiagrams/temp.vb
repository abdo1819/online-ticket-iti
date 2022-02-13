' Database staticdb

' user -> passenger : login(username, password)
' activate user
' activate passenger
' passenger -> staticdb : login(username, password)
' activate staticdb
' staticdb -> passenger : result
' deactivate staticdb
' passenger -> user : user info
' deactivate passenger

' activate user
' activate passenger


' user -> journey : get AvialableJourneys (departure, arival)
' activate journey
' journey -> trainStation : getTrains(departure,arrival)
' activate trainStation
' trainStation -> journey : available trains
' deactivate trainStation
'     loop [trains] 
'         journey -> train : AvialableSeats(train)
'         activate train
'         train -> journey : free seats
'         deactivate train
'     end
' journey -> user : dictionary<journey,tiers>
' deactivate journey
' loop [journeys]
'     user -> journey : getprice(journey,tier)
'     activate journey
'     journey -> trainStation : getLineDistance(station1,station2)
'     activate trainStation
'     trainStation -> journey : distance
'     deactivate trainStation
'     ' journey -> train : getUnitPrice(tier)
'     ' activate train
'     ' train -> journey : price
'     ' deactivate train
'     journey -> user : price
'     deactivate journey
' end