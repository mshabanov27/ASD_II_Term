class Graph:
    def __init__(self, weight_matrix):
        self.__weight_matrix = weight_matrix
        self.__flow_matrix = self.__weight_matrix

    def Edmonds_Karp(self, source, goal):
        max_flow = 0
        paths = []
        flows = []
        current_path = self.BFS(source, goal)
        while current_path is not None:
            paths.append(current_path[0])
            max_flow += current_path[2]
            flows.append(current_path[1])
            current_path = self.BFS(source, goal)
        return paths, flows, max_flow

    def BFS(self, source, goal):
        queue = [source]
        visited = [source]
        came_from = dict()
        came_from[source] = None
        continue_searching = True

        while queue and continue_searching:
            current = queue.pop(0)
            if current == goal:
                continue_searching = False
            for i, flow in enumerate(self.__flow_matrix[current]):
                if i not in queue and i not in visited and flow > 0 and i != current:
                    queue.append(i)
                    came_from[i] = current
        if goal in came_from:
            return self.reconstruct_path(came_from, source, goal)

    def reconstruct_path(self, came_from, start, goal):
        current = goal
        current_flow = self.__find_bottleneck(came_from, start, goal)
        path = [current]

        while current != start:
            self.__flow_matrix[came_from[current]][current] -= current_flow
            current = came_from[current]
            path.append(current)
        path.reverse()
        return path, current_flow, current_flow

    def __find_bottleneck(self, came_from, start, goal):
        bottleneck = self.__flow_matrix[came_from[goal]][goal]
        current = goal

        while current != start:
            if bottleneck > self.__flow_matrix[came_from[current]][current]:
                bottleneck = self.__flow_matrix[came_from[current]][current]
            current = came_from[current]
        return bottleneck
